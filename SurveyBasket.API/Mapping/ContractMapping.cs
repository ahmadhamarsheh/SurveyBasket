

using SurveyBasket.API.Contract.Request;
using SurveyBasket.API.Contract.Response;

namespace SurveyBasket.API.Mapping
{
    public static class ContractMapping
    {
        public static PollResponse MapToPollResponse(this Poll poll)
        {
            return new PollResponse(
                poll.Id,
                poll.Title,
                poll.Summary,
                true,
                new DateOnly(2025, 2, 15),
                new DateOnly(2025, 3, 1)
            );
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
                Summary = pollRequest.Summary,
            };
        }
    }
}
