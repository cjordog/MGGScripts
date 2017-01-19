using UnityEngine;
using System.Collections.Generic;

public abstract class PlayerAction {

	public PlayerAction(float duration = Mathf.Infinity, float cooldown = 0) {
		m_duration = duration;
		m_cooldown = cooldown;
	}

	public delegate void CollisionHandler(GameObject sender, Collision col);
	public delegate void TriggerHandler(GameObject sender, Collider other);

	public class CollisionHandlers {
		public event CollisionHandler enter = delegate {};
		public event CollisionHandler stay = delegate {};
		public event CollisionHandler exit = delegate{};

		public void Enter(GameObject sender, Collision col) {
			enter(sender, col);
		}

		public void Stay(GameObject sender, Collision col) {
			stay(sender, col);
		}

		public void Exit(GameObject sender, Collision col) {
			exit(sender, col);
		}
	}

	public class TriggerHandlers {
		public event TriggerHandler enter = delegate {};
		public event TriggerHandler stay = delegate {};
		public event TriggerHandler exit = delegate {};

		public void Enter(GameObject sender, Collider other) {
			enter(sender, other);
		}

		public void Stay(GameObject sender, Collider other) {
			stay(sender, other);
		}

		public void Exit(GameObject sender, Collider other) {
			exit(sender, other);
		}
	}

	public CollisionHandlers collisionHandlers = new CollisionHandlers();
	public TriggerHandlers triggerHandlers = new TriggerHandlers();

	private float m_duration;
	public float Duration {
		get { return m_duration; }
	}

	private float m_cooldown;
	public float Cooldown {
		get { return m_cooldown; }
	}


	public abstract void StartAction(PlayerMovement movement);

	public virtual void EndAction(PlayerMovement movement) {
		return;
	}


	protected void RegisterCollisionEnter(CollisionHandler handler,
		bool register = true) {
		if(!register) collisionHandlers.enter -= handler;
		else collisionHandlers.enter += handler;
	}

	protected void RegisterCollisionStay(CollisionHandler handler,
		bool register = true) {
		if(!register) collisionHandlers.stay -= handler;
		else collisionHandlers.stay += handler;
	}

	protected void RegisterCollisionExit(CollisionHandler handler,
		bool register = true) {
		if(!register) collisionHandlers.exit -= handler;
		else collisionHandlers.exit += handler;
	}

	protected void RegisterTriggerEnter(TriggerHandler handler,
		bool register = true) {
		if(!register) triggerHandlers.enter -= handler;
		else triggerHandlers.enter += handler;
	}

	protected void RegisterTriggerStay(TriggerHandler handler,
		bool register = true) {
		if(!register) triggerHandlers.stay -= handler;
		else triggerHandlers.stay += handler;
	}

	protected void RegisterTriggerExit(TriggerHandler handler,
		bool register = true) {
		if(!register) triggerHandlers.exit -= handler;
		else triggerHandlers.exit += handler;
	}
}
