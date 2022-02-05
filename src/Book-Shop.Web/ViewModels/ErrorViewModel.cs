namespace Book_Shop.Web.Models
{
    public class ErrorViewModel
    {
        public int ErrorCode { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public static class ErrorViewModelBuilder
    {
        private static readonly Dictionary<int, ErrorViewModel> _errorsAction = new Dictionary<int, ErrorViewModel>
        {
            { 500, BuildError500() },
            { 404, BuildError404() },
            { 403, BuildError403() }
        };

        public static ErrorViewModel BuildError(int errorCode)
        {
            if (!_errorsAction.ContainsKey(errorCode)) return null;

            return _errorsAction[errorCode];
        }

        private static ErrorViewModel BuildError500()
        {
            return new ErrorViewModel
            {
                Message = "An error occurred! Try again later.",
                Title = "Woow! Something went wrong! :-(",
                ErrorCode = 500
            };
        }

        private static ErrorViewModel BuildError404()
        {
            return new ErrorViewModel
            {
                Message = "The page that you are looking for doesn't exists!",
                Title = "Woow! Not found",
                ErrorCode = 404
            };
        }

        private static ErrorViewModel BuildError403()
        {
            return new ErrorViewModel
            {
                Message = "You don't have authorization to do this!",
                Title = "Access denied",
                ErrorCode = 403
            };
        }
    }
}