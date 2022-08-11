using DynamicBox.EventManagement;
using DynamicBox.GameEvents;
using UnityEngine;

namespace DynamicBox.Controllers
{
	public class CellController : MonoBehaviour
	{
		[Header ("Parameters")]
		[SerializeField] private int _cellId = 1;

		public int CellId => _cellId;

		void OnTriggerEnter (Collider other)
		{
			Debug.Log ($"{_cellId} cell was Entered");
			EventManager.Instance.Raise (new CellActivatedEvent (_cellId));
		}
	}
}