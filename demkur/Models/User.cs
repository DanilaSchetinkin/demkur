﻿using System;
using System.Collections.Generic;

namespace demkur.Models;

public partial class User
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public string Login { get; set; } = null!;

    public virtual Role RoleNavigation { get; set; } = null!;
}
