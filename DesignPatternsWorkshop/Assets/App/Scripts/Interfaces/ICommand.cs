namespace DynamicBox.Interfaces
{
	public interface ICommand
	{
		public void Execute ();

		public void Undo ();

		public float GetCommandEndTime ();
	}
}