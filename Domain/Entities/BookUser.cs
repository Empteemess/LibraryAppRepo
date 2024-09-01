﻿namespace Domain.Entities;

public class BookUser : BaseEntity
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }

    public Book? Book { get; set; }
    public User? User { get; set; }
}