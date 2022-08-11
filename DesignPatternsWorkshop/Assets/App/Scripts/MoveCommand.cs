using UnityEngine;

public class MoveCommand : ICommand
{
	public GameObject MovedObject;
	public Vector3 InitialPos;
	public Vector3 DestinationPos;
	public float Speed;


	public MoveCommand (GameObject movedObject, Vector3 initialPos, Vector3 destinationPos, float speed)
	{
		MovedObject = movedObject;
		InitialPos = initialPos;
		DestinationPos = destinationPos;
		Speed = speed;
	}

	public void Execute ()
	{
		CommandManager.Instance.MoveObject (MovedObject, DestinationPos, Speed);
	}

	public void Undo ()
	{
		CommandManager.Instance.MoveObject (MovedObject, InitialPos, Speed);
	}

	public float GetCommandEndTime ()
	{
		float distance = Vector3.Distance (InitialPos, DestinationPos);
		float estimatedTime = distance / Speed;
		return estimatedTime;
	}
}