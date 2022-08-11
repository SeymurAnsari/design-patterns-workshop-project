using DynamicBox.EventManagement;

namespace DynamicBox.GameEvents
{
    public class CellActivatedEvent : GameEvent
    {
        public readonly int CellId;

        public CellActivatedEvent (int cellId)
        {
            CellId = cellId;
        }
    }
}