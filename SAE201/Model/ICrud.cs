namespace SAE201.Model
{
    public interface ICrud<T>
    {
        int Create();
        void Read();
        int Update();
        int Delete();
        List<T> FindAll();
        List<T> FindBySelection(string criteres);
    }
}

