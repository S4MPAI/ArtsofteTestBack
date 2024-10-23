namespace DAL.Exceptions;

public class NotUniqueValueInPropertyException : Exception
{
    public Type ClassType {  get; }
    public string PropertyName { get; }
    public object PropertyValue {  get; }

    public NotUniqueValueInPropertyException(Type classType, string propertyName, object propertyValue, Exception? innerException = null) 
        : base($"Уже имеется такой объект типа {classType.Name} со значением {propertyValue} в поле {propertyName}", innerException)
    {
        ClassType = classType;
        PropertyName = propertyName;
        PropertyValue = propertyValue;
    }
}
