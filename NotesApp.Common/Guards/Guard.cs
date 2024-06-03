namespace NotesApp.Common.Guards;

/// <summary>
/// Provides guard clauses to validate method arguments.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Checks if the specified value is null and throws an <see cref="ArgumentNullException"/> if it is.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <param name="value">The value to check for null.</param>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    public static void GuardAgainstNull<T>(string paramName, T value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName, "can not be null");
        }
    }

    /// <summary>
    /// Checks if the specified collection is null or empty and throws an exception if either condition is met.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <param name="list">The collection to check for null or empty.</param>
    /// <exception cref="ArgumentNullException">Thrown when the collection is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the collection is empty.</exception>
    public static void GuardAgainstNullOrEmpty<T>(string paramName, IEnumerable<T> list)
    {
        if (list is null)
        {
            throw new ArgumentNullException(paramName, "can not be null");
        }
        else if (!list.Any())
        {
            throw new ArgumentException("can not be empty", paramName);
        }
    }
}

