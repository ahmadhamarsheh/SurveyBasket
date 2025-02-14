

using SurveyBasket.API.Contract.Request;
using SurveyBasket.API.Contract.Response;

namespace SurveyBasket.API.Mapping
{
    public static class ContractMapping
    {
        public static PollResponse MapToPollResponse(this Poll poll)
        {
            return new PollResponse
            {
                Id = poll.Id,
                Title = poll.Title,
                Notes = poll.Description,
            };
        }

        public static IEnumerable<PollResponse> MapAllToPollResponse(this IEnumerable<Poll> polls)
        {
            return polls.Select(MapToPollResponse);
        }

        public static Poll MapToPoll(this PollRequest pollRequest)
        {
            return new Poll
            {
                Title = pollRequest.Title,
                Description = pollRequest.Description,
            };
        }
    }
}
