using BookAPI.Commons;
using BookAPI.Models;

namespace BookAPI.Storage
{
    public class MemCache : IStorage<Book>
    {
        private object _sync = new object();
        private List<Book> _memCache = new List<Book>();
        public Book this[Guid id]
        {
            get
            {
                lock (_sync)
                {
                    if (!Has(id))
                    {
                        throw new IncorrectBookException($"No book with id {id}");
                    }

                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty)
                {
                    throw new IncorrectBookException("Cannot request book with an empty id");
                }

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _memCache.Add(value);
                }
            }
        }

        public List<Book> All => _memCache.Select(x => x).ToList();

        public void Add(Book value)
        {
            if (value.Id != Guid.Empty)
            {
                throw new IncorrectBookException($"Cannot add value with predefined id {value.Id}");
            }

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _memCache.Any(x => x.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _memCache.RemoveAll(x => x.Id == id);
            }
        }
    }

}
