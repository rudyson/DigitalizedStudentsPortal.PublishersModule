﻿using FPECS.DSP.SPW.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.Business.Helpers;
public static class ResearcherHelper
{
    public static string GetResearcherShortName(Researcher researcher)
    {
        var result = new StringBuilder(researcher.LastName);
        result.Append($" {researcher.FirstName.ToUpper()[0]}.");
        if (!string.IsNullOrEmpty(researcher.MiddleName))
        {
            result.Append($" {researcher.MiddleName.ToUpper()[0]}.");
        }
        return result.ToString();
    }
}
