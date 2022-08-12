using DynamicBox.EventManagement;
using DynamicBox.GameEvents;
using DynamicBox.UI.Views;
using UnityEngine;

namespace DynamicBox.UI.Controllers
{
	public class LoseViewController : MonoBehaviour
	{
		[SerializeField] private LoseView _view;

		void OnEnable ()
		{
			EventManager.Instance.AddListener<PlayerLoseEvent> (PlayerLoseHandler);
		}

		void OnDisable ()
		{
			EventManager.Instance.RemoveListener<PlayerLoseEvent> (PlayerLoseHandler);
		}

		private void PlayerLoseHandler (PlayerLoseEvent eventDetail)
		{
			_view.gameObject.SetActive (true);
		}

		public void OnTryAgainButtonPressed ()
		{
			_view.gameObject.SetActive (false);
			EventManager.Instance.Raise (new ResetGameEvent ());
		}
	}
}