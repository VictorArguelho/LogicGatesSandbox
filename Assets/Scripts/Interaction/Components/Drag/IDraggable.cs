public interface IDraggable
{
    public bool IsDragging { get; }
    public void StartDragging();
    public void StopDragging();
}