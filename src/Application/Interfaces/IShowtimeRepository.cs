using Theater_Management_BE.src.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IShowtimeRepository
    {
        // Thêm showtime mới
        Showtime Add(Showtime showtime);

        // Lấy showtime theo ID
        Showtime? GetById(Guid id);

        // Lấy showtime theo trường bất kỳ (ví dụ MovieId, AuditoriumId, StartTime)
        Showtime? GetByField(string fieldName, object value);

        // Cập nhật theo ID và tên trường
        bool UpdateByField(Guid id, string fieldName, object value);

        // Xoá showtime theo ID
        bool Delete(Guid id);

        // Lấy tất cả showtime
        List<Showtime> GetAll();

        // Xoá tất cả showtime
        void DeleteAll();
    }
}
