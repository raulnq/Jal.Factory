using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.NUnit2;

namespace Jal.Factory.Tests.Attribute
{
    public class AutoDataBuilderAttribute : AutoDataAttribute
    {
        public AutoDataBuilderAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {

        }
    }
}
