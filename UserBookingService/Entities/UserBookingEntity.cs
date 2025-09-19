using System.ComponentModel.DataAnnotations;

namespace UserBookingService.Entities;

public class UserBookingEntity
{
    [Key]
    public Guid BookingId { get; set; } = Guid.NewGuid();

}

