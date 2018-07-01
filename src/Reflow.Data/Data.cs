namespace Reflow.Data
{
    public class Data
    {
        private static ReflowContext _context;

        public static ReflowContext Context => _context ?? (_context = new ReflowContext());
    }
}
