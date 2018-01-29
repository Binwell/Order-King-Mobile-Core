
namespace OrderKingCoreDemo.DAL {
	public struct RequestResult<T> where T: class {
		public readonly string Message;
		public readonly RequestStatus Status;
		public readonly T Data;
		public bool IsValid => Status == RequestStatus.Ok && Data != null;

		public RequestResult(T data, RequestStatus status, string message = null) {
			Data = data;
			Status = status;
			Message = message;
		}

		public override string ToString() { return $@"Result: {Status}, Data: {Data}, Message: {Message}"; }
	}

	public struct RequestResult {
		public readonly string Message;
		public readonly RequestStatus Status;
		public bool IsValid => Status == RequestStatus.Ok;

		public RequestResult(RequestStatus status, string message = null) {
			Status = status;
			Message = message;
		}

		public override string ToString() { return $@"Result: {Status},  Message: {Message}"; }
	}


	public enum RequestStatus {
		Unknown = 0,
		Ok = 200,
		NotModified = 304,
		BadRequest = 400,
		Unauthorized = 401,
		Forbidden = 403,
		NotFound = 404,
		InternalServerError = 500,
		ServiceUnavailable = 503,
		Canceled = 1001,
		InvalidRequest = 1002,
		SerializationError = 1003,
		DatabaseError = 1100,
		FileAccessError = 2001
	}
}
