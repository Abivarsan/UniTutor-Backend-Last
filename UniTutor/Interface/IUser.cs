namespace UniTutor.Interface
{
    public interface IUser
    {
        Task<IEnumerable<object>> GetLastJoinedUsersAsync(int count);
    }
}
