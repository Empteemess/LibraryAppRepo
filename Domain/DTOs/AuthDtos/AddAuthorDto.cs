﻿namespace Domain.DTOs.AuthDtos;

public class AddAuthorDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
}