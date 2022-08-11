using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
	public static CommandManager Instance;

	private List<ICommand> executedCommands = new List<ICommand> ();

	private bool IsUndoing;

	private IEnumerator moveCoroutine;

	#region Unity Methods

	void Awake ()
	{
		Instance = this;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space) && !IsUndoing)
		{
			UndoAllCommands();
		}
	}

	#endregion

	private void UndoAllCommands ()
	{
		IsUndoing = true;
		StartCoroutine (UndoCoroutine ());
	}

	private IEnumerator UndoCoroutine ()
	{
		for (int i = executedCommands.Count - 1; i >= 0; i--)
		{
			executedCommands[i].Undo ();

			yield return new WaitForSeconds (executedCommands[i].GetCommandEndTime ());
		}
		executedCommands.Clear ();
		IsUndoing = false;
	}

	public void AddCommandToList (ICommand command)
	{
		executedCommands.Add (command);
	}

	public void MoveObject (GameObject movedObject, Vector3 destinationPoint, float speed)
	{
		if (moveCoroutine != null)
		{
			StopCoroutine (moveCoroutine);
		}

		moveCoroutine = MovedObjectCoroutine (movedObject, destinationPoint, speed);

		StartCoroutine (moveCoroutine);
	}

	private IEnumerator MovedObjectCoroutine (GameObject movedObject, Vector3 destinationPoint, float speed)
	{
		while (movedObject.transform.position != destinationPoint)
		{
			movedObject.transform.position = Vector3.MoveTowards (movedObject.transform.position, destinationPoint, speed * Time.deltaTime);

			yield return new WaitForEndOfFrame ();
		}
	}
}