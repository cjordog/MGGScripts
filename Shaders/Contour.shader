Shader "Custom/Contour"
{
	Properties
	{
		// _MainTex ("Texture", 2D) = "white" {}
		_Scale ("Scale", Range(0, 4)) = 1
		_TangentColor ("Tangent Color", Color) = (0, 0, 0, 1)
		_NormalColor ("Normal Color", Color) = (1, 1, 1, 1)
		[NoScaleOffset] _NormalMap ("Normal Map", 2D) = "" {}
		_BumpScale ("Bumpiness", Float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			Name "BASE"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			// #pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "UnityStandardBRDF.cginc"
			#include "UnityStandardUtils.cginc"

			half _Scale;
			float4 _TangentColor;
			float4 _NormalColor;
			sampler2D _NormalMap;
			float _BumpScale;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float3 normal : TEXCOORD1;
				float4 tangent : TEXCOORD2;
				float3 worldPos : TEXCOORD3;
				// UNITY_FOG_COORDS(0)
				float4 vertex : SV_POSITION;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.tangent = float4(UnityObjectToWorldDir(v.tangent.xyz), v.tangent.w);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.uv = v.uv;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			half4 frag (v2f i) : SV_Target
			{
				float3 tangentSpaceNormal =
					UnpackScaleNormal(tex2D(_NormalMap, i.uv), _BumpScale);

				float3 binormal = cross(i.normal, i.tangent.xyz) *
					(i.tangent.w * unity_WorldTransformParams.w);

				i.normal = normalize(
					tangentSpaceNormal.x * i.tangent +
					tangentSpaceNormal.y * binormal +  // Do YZ swap here
					tangentSpaceNormal.z * i.normal
				);

				float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
				half contour = pow(DotClamped(i.normal, viewDir), _Scale);
				half4 colComp = lerp(_TangentColor, _NormalColor, contour);
				half4 col = fixed4(colComp.r, colComp.g, colComp.b, 1);
				// apply fog
				// UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
