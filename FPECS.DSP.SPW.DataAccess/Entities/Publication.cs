﻿using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities;

public class Publication
{
    public required long Id { get; set; }
    public required string Title { get; set; } // DSTU
    public required PublicationTypes Type { get; set; }
    public required PublicationCategory Category { get; set; }
    public DateOnly Year { get; set; }
    public short? Pages { get; set; } // TotalPages
    public short? PagesAuthor { get; set; }
    public string? PublishingName { get; set; }


    #region Conference - Theses

    public bool IsWithStudent { get; set; }
    public bool IsInternational { get; set; }
    public string? ConferenceName { get; set; }
    public DateTime? ConferenceStartDate { get; set; }
    public DateTime? ConferenceEndDate { get; set; }
    public string? ConferenceCountry { get; set; }
    public string? ConferenceCity { get; set; }

    #endregion

    #region Magazine - Article

    public string? MagazineName { get; set; }
    public string? MagazineIssue { get; set; } // Випуск
    public string? MagazineNumber { get; set; } // Номер тому
    public short? PageFirst { get; set; }
    public short? PageLast { get; set; }

    #endregion

    #region Metadata

    public string? Doi { get; set; }
    public string? Isbn { get; set; }
    public string? Issn { get; set; }
    public string? Url { get; set; }

    #endregion

    #region Relations

    public virtual List<PublicationPublisher>? PublicationPublishers { get; set; }
    public virtual List<PublicationExternalPublisher>? PublicationExternalPublishers { get; set; }
    public virtual List<Discipline>? Disciplines { get; set; }

    #endregion
}