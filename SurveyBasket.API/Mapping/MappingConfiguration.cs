﻿using SurveyBasket.API.Contract.Polls;

namespace SurveyBasket.API.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Poll, PollResponse>()
                .Map(dest => dest.Summary, src => src.Summary);
        }
    }
}
