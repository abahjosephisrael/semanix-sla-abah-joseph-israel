namespace FormsEngine.Shared.Exceptions;


public class HttpNotFoundException(string message) : Exception(message)
{
}

public class HttpConflictException(string message) : Exception(message)
{
}

public class HttpServerErrorException(string message) : Exception(message)
{
}
public class HttpUnauthorisedException(string message) : Exception(message)
{
}

public class HttpBadRequestException(string message) : Exception(message)
{
}

public class HttpBadForbiddenException(string message) : Exception(message)
{
}