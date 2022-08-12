using DynamicBox.Commands;
using DynamicBox.EventManagement;
using DynamicBox.GameEvents;
using DynamicBox.Managers;
using UnityEngine;

namespace DynamicBox.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		[Header ("Parameters")]
		[SerializeField] private int speed = 1;

		private bool canMove = true;

		void OnEnable ()
		{
			EventManager.Instance.AddListener<CommandUndoDoneEvent> (CommandUndoDoneHandler);
			EventManager.Instance.AddListener<PlayerWinEvent> (PlayerWinHandler);
			EventManager.Instance.AddListener<PlayerLoseEvent> (PlayerLoseHandler);
		}

		void OnDisable ()
		{
			EventManager.Instance.RemoveListener<CommandUndoDoneEvent> (CommandUndoDoneHandler);
			EventManager.Instance.RemoveListener<PlayerWinEvent> (PlayerWinHandler);
			EventManager.Instance.RemoveListener<PlayerLoseEvent> (PlayerLoseHandler);
		}

		void Update ()
		{
			if (Input.GetMouseButtonUp (0) && canMove)
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (ray, out RaycastHit hit))
				{
					MoveCommand command = new MoveCommand (gameObject, transform.position, hit.point, speed);

					command.Execute ();
					CommandManager.Instance.AddCommandToList (command);
				}
			}
		}

		#region Event Handlers

		private void CommandUndoDoneHandler (CommandUndoDoneEvent eventDetails)
		{
			canMove = true;
		}

		private void PlayerWinHandler (PlayerWinEvent eventDetails)
		{
			canMove = false;
		}

		private void PlayerLoseHandler (PlayerLoseEvent eventDetails)
		{
			canMove = false;
		}

		#endregion
	}
}