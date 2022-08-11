using DynamicBox.EventManagement;
using DynamicBox.GameEvents;
using UnityEngine;

public class CellController : MonoBehaviour
{
	[Header ("Parameters")]
	[SerializeField] private int cellId = 1;

	void OnTriggerEnter (Collider other)
	{
		Debug.Log ($"{cellId} cell was Entered");
		EventManager.Instance.Raise (new CellActivatedEvent (cellId));
	}
}