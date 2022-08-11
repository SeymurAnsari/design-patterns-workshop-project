using DynamicBox.Commands;
using DynamicBox.Managers;
using UnityEngine;

namespace DynamicBox.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		[Header("Parameters")]
		[SerializeField] private int speed = 1;

		void Update ()
		{
			if (Input.GetMouseButtonUp (0))
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
	}
}