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

		private void SetCellColor (CellController cell, Color color)
		{
			var cubeRenderer = cell.GetComponent<Renderer> ();
			cubeRenderer.material.SetColor ("_Color", color);
		}

		private void ResetCellColors ()
		{
			foreach (var cell in cells)
			{
				SetCellColor (cell,Color.white);
			}
		}

		#region Event Handlers

		private void CellActivatedHandler (CellActivatedEvent eventDetails)
		{
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
				Debug.Log ("You LOSE / Try Again");
				activatedCellNumber = 0;
				SetCellColor (currentCell,Color.red);
				ResetCellColors ();
			}
			else
			{
				SetCellColor (currentCell,Color.green);
				if (activatedCellNumber is 3)
				{
					Debug.Log ("You win");
					activatedCellNumber = 0;
					ResetCellColors ();
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
}