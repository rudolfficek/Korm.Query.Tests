using Kros.KORM;
using Kros.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Korm.Query.Tests
{
    public class QueryProvider
    {
        private readonly IDatabase _database;
        public QueryProvider(IDatabase database)
            => _database = Check.NotNull(database, nameof(database));

        public IEnumerable<T> GetWithKormQueryBuilder<T>()
            => _database.Query<T>().ToList();

        public IEnumerable<T> GetNotNullableByQuery<T>()
            => _database.Query<T>()
                .Select("Id", "NotNullableColumn")
                .From("NotNullables")
                .ToList();

        public IEnumerable<T> GetNullableButWithNotNullOnlyByQuery<T>(long? id = null)
            => _database.Query<T>()
                .Select("Id", "NotNullableColumn", "NullableColumn")
                .From("Nullables")
                .Where("NullableColumn IS NOT NULL")
                .ToList();

        public IEnumerable<T> GetNotNullableByQuery<T>(long? id = null)
            => _database.Query<T>()
                .Select("*")
                .From("NotNullables")
                .Where("@id IS NULL OR Id = @id", id)
                .ToList();

        public T GetReservationByIdQuery<T>(long id)
            => _database.Query<T>()
                .Select("Id", "Name")
                .From("Reservations")
                .Where("Id = @id", id)
                .FirstOrDefault();

        public IEnumerable<T> GetReservedSeatsByQuery<T>()
            => _database.Query<T>()
                .Select("s.Id AS SeatId"
                    , "s.Number AS SeatNumber"
                    , "r.Id AS ReservationId"
                    , "r.Name")
                .From("Seats s INNER JOIN Reservations r ON (s.ReservationId = r.Id)")
                .ToList();

        public IEnumerable<T> GetAllSeatsQuery<T>()
            => _database.Query<T>()
                .Select("Id"
                    , "Number"
                    , "ReservationId")
                .From("Seats")
                .ToList();

        public IEnumerable<T> GetAllSeatsWithReservationName<T>()
            => _database.Query<T>()
                .Select("s.Id AS SeatId"
                    , "s.Number AS SeatNumber"
                    , "r.Id AS ReservationId"
                    , "r.Name AS Name")
                .From("Seats s LEFT JOIN Reservations r ON (s.ReservationId = r.Id)")
                .ToList();

        public IEnumerable<T> GetAllSeatsWithReservationNameSql<T>()
            => _database.Query<T>()
                .Sql(@"SELECT s.Id AS SeatId, s.Number AS SeatNumber, r.Id AS ReservationId, r.Name AS Name
                            FROM Seats s LEFT JOIN Reservations r ON (s.ReservationId = r.Id)")
                .ToList();
    }
}
