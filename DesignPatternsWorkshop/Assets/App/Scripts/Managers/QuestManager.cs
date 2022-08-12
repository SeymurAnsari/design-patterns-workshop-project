using System.Collections.Generic;
using DynamicBox.Controllers;
using DynamicBox.EventManagement;
using DynamicBox.GameEvents;
using UnityEngine;

namespace DynamicBox.Managers
{
	public class QuestManager : MonoBehaviour
	{
		[Header ("Links")]
		[SerializeField] private List<CellController> cells;

		private CellController currentCell;

		private int[] correctSequence = new int[] {3, 1, 0, 2};
		private int activatedCellNumber;

		private bool canCheck=true;

		#region Unity Methods

		void OnEnable ()
		{
			EventManager.Instance.AddListener<CellActivatedEvent> (CellActivatedHandler);
			EventManager.Instance.AddListener<CommandUndoDoneEvent> (CommandUndoDoneHandler);
		}

		void OnDisable ()
		{
			EventManager.Instance.RemoveListener<CellActivatedEvent> (CellActivatedHandler);
			EventManager.Instance.RemoveListener<CommandUndoDoneEvent> (CommandUndoDoneHandler);
		}

		#endregion

		private void SetCellColor (CellController cell, Color color)
		{
			var cubeRenderer = cell.GetComponent<Renderer> ();
			cubeRenderer.material.SetColor ("_Color", color);
		}

		private void ResetCells ()
		{
			activatedCellNumber = 0;
			
			foreach (var cell in cells)
			{
				SetCellColor (cell, Color.white);
			}
		}

		#region Event Handlers

		private void CellActivatedHandler (CellActivatedEvent eventDetails)
		{
			if (!canCheck)
			{
				return;
			}

			foreach (var cell in cells)
			{
				if (eventDetails.CellId == cell.CellId)
				{
					currentCell = cell;
					break;
				}
			}

			if (eventDetails.CellId != correctSequence[activatedCellNumber])
			{
				EventManager.Instance.Raise (new PlayerLoseEvent ());

				Debug.Log ("You LOSE / Try Again");
				SetCellColor (currentCell, Color.red);
				canCheck = false;
			}
			else
			{
				SetCellColor (currentCell, Color.green);
				if (activatedCellNumber is 3)
				{
					EventManager.Instance.Raise (new PlayerWinEvent ());
					Debug.Log ("You win");
					canCheck = false;
				}
				else
				{
					Debug.Log ("Well Done. Contunue");
					activatedCellNumber++;
				}
			}
		}
		
		private void CommandUndoDoneHandler (CommandUndoDoneEvent eventDetails)
		{
			ResetCells ();
			canCheck = true;
		}

		#endregion
	}
}