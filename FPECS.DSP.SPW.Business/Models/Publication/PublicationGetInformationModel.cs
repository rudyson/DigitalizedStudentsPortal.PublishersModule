using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using FPECS.DSP.SPW.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.Business.Models.Publication;
public record PublicationGetInformationModel(
    long Id,
    string Title,
    string Reference,
    PublicationTypes Type,
    PublicationCategory Category,
    DateOnly Year,
    string? Url,
    List<PublicationContributorModel> Contributors,
    List<PublicationContributorModel> ExternalContributors);