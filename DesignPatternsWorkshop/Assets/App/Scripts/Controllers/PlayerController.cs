using UnityEngine;

public class PlayerController : MonoBehaviour
{
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