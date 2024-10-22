namespace DAL.Exceptions;

public class NotUniqueValueInProperyException : Exception
{
    public Type ClassType {  get; }
    public string PropertyName { get; }
    public object PropertyValue {  get; }
    public override string Message => GetMessage();

    public NotUniqueValueInProperyException(Type classType, string propertyName, string propertyValue)
    {
        ClassType = classType;
        PropertyName = propertyName;
        PropertyValue = propertyValue;
    }

    private string GetMessage()
    {
        return $"Уже имеется такой объект типа {ClassType.Name} со значением {PropertyValue.ToString()} в поле {PropertyName}";
    }
}
