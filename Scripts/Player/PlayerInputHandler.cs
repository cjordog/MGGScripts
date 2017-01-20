using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour {

	struct MovementAxes
	{
		public string horizontal;
		public string vertical;

		public MovementAxes(string h, string v)
		{
			horizontal = h;
			vertical = v;
		}

		public static void Swap(ref MovementAxes a, ref MovementAxes b)
		{
			MovementAxes temp = b;
			b = a;
			a = temp;
		}
	}

	struct ActionButtons
	{
		public string action1;
		public string action2;

		public ActionButtons(string a1, string a2)
		{
			action1 = a1;
			action2 = a2;
		}

		public static void Swap(ref ActionButtons a, ref ActionButtons b)
		{
			ActionButtons temp = b;
			b = a;
			a = temp;
		}
	}


	[Tooltip("The \"number\" of the player. " +
		"Represents which controller or keyboard controls to use. " +
		"May not correspond to who is considered \"first player.\"")]
	[Range(1, 4)]
	[SerializeField]
	private int controlNumber = 1;
	public int ControlNumber {
		get { return controlNumber; }
		set {
			controlNumber = value;
			ResetInputNames();
		}
	}

	[Tooltip("Whether or not this player's input should be gotten " +
		"from a controller")]
	[SerializeField]
	private bool useController = true;
	public bool UseController {
		get { return useController; }
		set {
			useController = value;
			ResetInputNames();
		}
	}

	private int PlayerIndex {
		get { return controlNumber - 1; }
	}

	public struct ControlSettings {
		public int num;
		public bool useController;

		public ControlSettings(int _num, bool _useController) {
			num = _num;
			useController = _useController;
		}
	}

	private MovementAxes movement;
	private ActionButtons actions;


	void Awake()
	{
		ResetInputNames();
	}

	public float GetHorizontal()
	{
		return Input.GetAxisRaw(movement.horizontal);
	}

	public float GetVertical()
	{
		return Input.GetAxisRaw(movement.vertical);
	}

	public bool GetAction1()
	{
		return Input.GetButtonDown(actions.action1);
	}

	public bool GetAction2()
	{
		return Input.GetButtonDown(actions.action2);
	}

	public static List<ControlSettings> GetAllAction1() {
		return GetAllAction(0);
	}

	public static List<ControlSettings> GetAllAction2() {
		return GetAllAction(1);
	}

	private static List<ControlSettings> GetAllAction(int index) {
		List<ControlSettings> pressedSettings = new List<ControlSettings>();

		for(int i = 0; i < 4; i++) {
			for(int j = 0; j < 2; j++) {
				bool useControllerSetting = false;
				if(j == 1) useControllerSetting = true;

				ControlSettings checkSettings = new ControlSettings(i, useControllerSetting);
				bool pressed = GetAction(index, checkSettings);

				if(pressed) {
					pressedSettings.Add(checkSettings);
				}
			}
		}

		return pressedSettings;
	}

	private static bool GetAction(int actionIndex, ControlSettings settings) {
		int actionNum = actionIndex + 1;
		int controllerNum = settings.num + 1;
		bool useController = settings.useController;
		string[] joystickNames = Input.GetJoystickNames();
		if(settings.useController && settings.num >= joystickNames.Length) {
			return false;
		}

		string suffix = controllerNum.ToString();
		suffix += settings.useController ? "-GP" : "";

		if(settings.useController && (joystickNames[settings.num] ==
			"Sony Computer Entertainment Wireless Controller" ||
			joystickNames[settings.num] == "Unknown Wireless Controller")) {
			if(actionNum == 1) suffix += "-SONY";
			// Debug.Log("Action2-" + suffix);
			return Input.GetButtonDown("Action2-" + suffix);
		}
		else {
			// Debug.Log("Action" + actionNum + "-" + suffix);
			return Input.GetButtonDown("Action" + actionNum + "-" + suffix);
		}
	}

	public void ResetInputNames()
	{
		string[] joystickNames = Input.GetJoystickNames();
		if(useController && controlNumber > joystickNames.Length) {
			Debug.LogWarning(joystickNames.Length + " controllers are connected\n" +
				"Cannot use player number " + controlNumber + " with a controller");
			actions.action1 = actions.action2 = "";
			return;
		}

		string suffix = useController ? "-GP" : "";

		movement.horizontal = "LeftHorizontal-" + controlNumber +
			suffix;
		movement.vertical = "LeftVertical-" + controlNumber +
			suffix;

		if(useController && (joystickNames[PlayerIndex] ==
			"Sony Computer Entertainment Wireless Controller" ||
			joystickNames[PlayerIndex] == "Unknown Wireless Controller")) {
			actions.action1 = "Action2-" + controlNumber + suffix;
			actions.action2 = "Action2-" + controlNumber + suffix + "-SONY";
		}
		else {
			actions.action1 = "Action1-" + controlNumber + suffix;
			actions.action2 = "Action2-" + controlNumber + suffix;
		}
	}

	private void SwapMovementAxes(ref MovementAxes other)
	{
		MovementAxes.Swap(ref movement, ref other);
	}

	private void SwapActionButtons(ref ActionButtons other)
	{
		ActionButtons.Swap(ref actions, ref other);
	}
}