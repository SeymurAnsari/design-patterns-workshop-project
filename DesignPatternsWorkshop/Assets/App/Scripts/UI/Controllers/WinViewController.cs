using DynamicBox.EventManagement;
using DynamicBox.GameEvents;
using DynamicBox.UI.Views;
using UnityEngine;

namespace DynamicBox.UI.Controllers
{
	public class WinViewController : MonoBehaviour
	{
		[SerializeField] private WinView _view;

		void OnEnable ()
		{
			EventManager.Instance.AddListener<PlayerWinEvent> (PlayerWinHandler);
		}

		void OnDisable ()
		{
			EventManager.Instance.RemoveListener<PlayerWinEvent> (PlayerWinHandler);
		}

		private void PlayerWinHandler (PlayerWinEvent eventDetail)
		{
			_view.gameObject.SetActive (true);
		}

		public void OnPlayAgainButtonPressed ()
		{
			EventManager.Instance.Raise (new ResetGameEvent ());
			_view.gameObject.SetActive (false);
		}
	}
}