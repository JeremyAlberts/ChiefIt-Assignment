using YakShop.Core.Constants;
using YakShop.Core.Enumerations;

namespace YakShop.Core.Entities
{
    public class Yak
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public decimal Age { get; set; }
        public decimal AgeLastShaved { get; set; }
        public decimal CanBeShavedAt { get; set; }
        public bool IsDead { get; set; }
        public Sex Sex { get; set; }
        public int AgeInDays => (int)(Age * YakConstants.DaysInYakYear);

        public Yak() { }

        public Yak(string name, decimal age, Sex sex)
        { 
            Name = name;
            Age = age;
            Sex = sex;

            if (age > 0.99m) {
                AgeLastShaved = age;
                SetCanBeShavedAt();
            } else
            {
                AgeLastShaved = 0;
                CanBeShavedAt = 1;
            }
        }

        public void SetCanBeShavedAt()
        {
            decimal daysUntilShave = (YakConstants.MinimumShaveDays + AgeLastShaved) / YakConstants.DaysInYakYear;
            decimal roundedDays = Math.Floor(daysUntilShave * 100) / 100;
            CanBeShavedAt = AgeLastShaved + roundedDays;
        }

        public void SetAge(decimal age)
        {
            if (age <= YakConstants.YakDeathAge && !IsDead)
                Age = age;

            Died();

        }

        public void SetAgeLastShaved(decimal ageLastShaved)
        {
            AgeLastShaved = ageLastShaved;
        }

        public void Died()
        {
            IsDead = true;
        }

        public decimal Milk()
        {
            if (!IsDead)
            {
                decimal milk = YakConstants.MaximumMilkAmount - (AgeInDays * YakConstants.MilkDayMultiplier);

                return milk;
            }

            return 0;
        }

        public int Shave()
        {
            bool isOldEnough = Age >= YakConstants.MinimumShaveAge;
            bool needsShaving = Age >= CanBeShavedAt;

            if (isOldEnough && !IsDead && needsShaving)
            {
                AgeLastShaved = Age;
                SetCanBeShavedAt();
                return 1;
            }

            return 0;
        }

    }
}
