using System.Collections.Generic;
using DynamicBox.EventManagement;
using DynamicBox.GameEvents;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	[Header ("Links")]
	[SerializeField] private List<CellController> cells;

	private int[] correctSequence = new int[] {3,1,0,2};
	private int activatedCellNumber;

	#region Unity Methods
	
	void OnEnable ()
	{
		EventManager.Instance.AddListener<CellActivatedEvent> (CellActivatedHandler);
	}

	void OnDisable ()
	{
		EventManager.Instance.RemoveListener<CellActivatedEvent> (CellActivatedHandler);
	}
	
	#endregion
	
	#region Event Handlers

	private void CellActivatedHandler (CellActivatedEvent eventDetails)
	{
		if (eventDetails.CellId != correctSequence[activatedCellNumber])
		{
			Debug.Log ("You LOSE / Try Again");
			activatedCellNumber = 0;
		}
		else
		{
			if (activatedCellNumber is 3)
			{
				Debug.Log ("You win");
				activatedCellNumber = 0;
			}
			else
			{
				Debug.Log ("Well Done. Contunue");
				activatedCellNumber++;
			}
		}
	}

	#endregion
}
