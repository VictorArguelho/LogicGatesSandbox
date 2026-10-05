public readonly struct CircuitSaveResult 
{ 
    public CircuitSaveStatus Status { get; } 
    public string DirectoryName { get; } 

    public bool Succeeded => Status == CircuitSaveStatus.Success; 

    public CircuitSaveResult(CircuitSaveStatus status, string directoryName) 
    { 
        Status = status; 
        DirectoryName = directoryName; 
    } 

    public static CircuitSaveResult InvalidName => new(CircuitSaveStatus.InvalidName, string.Empty); 
}