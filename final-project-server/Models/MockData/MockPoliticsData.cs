using final_project_server.Models.Politics;

namespace final_project_server.Models.MockData
{
	public class MockPoliticsData
	{
		public static List<PoliticPolicy> GetMockPolicies = new List<PoliticPolicy>
		{
			new PoliticPolicy
			{
				Title = "Environmental Policy",
				Subtitle = "Sustainable Future",
				Description = "Aims to protect the environment and promote sustainability.",
				PoliticalLean = new List<PoliticalEnum> { PoliticalEnum.Left, PoliticalEnum.Right }
			},
			new PoliticPolicy
			{
				Title = "Economic Reform",
				Subtitle = "Fostering Growth",
				Description = "Focuses on economic growth and development.",
				PoliticalLean = new List<PoliticalEnum> { PoliticalEnum.EconomicLeft, PoliticalEnum.EconomicRight }
			},
			new PoliticPolicy
			{
				Title = "Social Equality",
				Subtitle = "Inclusive Society",
				Description = "Strives for social equality and inclusivity.",
				PoliticalLean = new List<PoliticalEnum> { PoliticalEnum.Left }
			},
			new PoliticPolicy
			{
				Title = "Liberty and Freedom",
				Subtitle = "Individual Rights",
				Description = "Emphasizes individual liberties and freedom.",
				PoliticalLean = new List<PoliticalEnum> { PoliticalEnum.Right }
			}
		};
	}
}
