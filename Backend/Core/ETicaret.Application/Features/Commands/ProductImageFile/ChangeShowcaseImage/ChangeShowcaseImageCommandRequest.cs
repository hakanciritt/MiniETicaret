﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ETicaret.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandRequest : IRequest<ChangeShowcaseImageCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
