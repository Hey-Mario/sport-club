using System.Collections.Generic;

namespace SportClub
{
    public interface IService<T>
    {
        void Create();
        List<T> ReadAll();
        T ReadById(int id);
        void Update();
        void Delete(int id);
    }
} 