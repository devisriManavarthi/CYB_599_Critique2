using AntMe.English;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntMe.Player.FirstAntColony
{
    [Player(
        ColonyName = "FirstAntColony",
        FirstName = "",
        LastName = ""
    )]
    [Caste(
        Name = "Default",
        AttackModifier = 0,
        EnergyModifier = 0,
        LoadModifier = 0,
        RangeModifier = 0,
        RotationSpeedModifier = 0,
        SpeedModifier = 0,
        ViewRangeModifier = 0
    )]
    public class FirstAntColonyClass : BaseAnt
    {
        private Sugar sugar = null;
        private Anthill anthill = null;
        private Item DestOptimized = null;

        #region Caste

        /// <summary>
        /// Every time that a new ant is born, its job group must be set. You can 
        /// do so with the help of the value returned by this method.
        /// Read more: "http://wiki.antme.net/en/API1:ChooseCaste"
        /// </summary>
        /// <param name="typeCount">Number of ants for every caste</param>
        /// <returns>Caste-Name for the next ant</returns>
        public override string ChooseCaste(Dictionary<string, int> typeCount)
        {
            return "Default";
        }

        #endregion

        #region Movement

        /// <summary>
        /// If the ant has no assigned tasks, it waits for new tasks. This method 
        /// is called to inform you that it is waiting.
        /// Read more: "http://wiki.antme.net/en/API1:Waiting"
        /// </summary>
        public override void Waiting()
        {
            if (anthill == null)
            {
                GoToAnthill();
                anthill = Destination as Anthill;
                Stop();
            }
            if (sugar != null && sugar.Amount > 0)
            {
                GoToDestOptimized(sugar);
            }
            else
            {
                GoForward(100);
            }
        }

        /// <summary>
        /// This method is called when an ant has travelled one third of its 
        /// movement range.
        /// Read more: "http://wiki.antme.net/en/API1:GettingTired"
        /// </summary>
        public override void GettingTired()
        {
            GoToAnthill();
        }

        /// <summary>
        /// This method is called if an ant dies. It informs you that the ant has 
        /// died. The ant cannot undertake any more actions from that point forward.
        /// Read more: "http://wiki.antme.net/en/API1:HasDied"
        /// </summary>
        /// <param name="kindOfDeath">Kind of Death</param>
        public override void HasDied(KindOfDeath kindOfDeath)
        {
        }

        /// <summary>
        /// This method is called in every simulation round, regardless of additional 
        /// conditions. It is ideal for actions that must be executed but that are not 
        /// addressed by other methods.
        /// Read more: "http://wiki.antme.net/en/API1:Tick"
        /// </summary>
        public override void Tick()
        {
            if (Destination == null && DestOptimized != null)
            {
                int distance = Coordinate.GetDistanceBetween(this, DestOptimized);
                if (distance < Viewrange)
                {
                    if (anthill == DestOptimized)
                    {
                        GoToDestination(DestOptimized);
                        DestOptimized = null;
                    }
                    //{
                    //    GoToAnthill();
                    //} 
                    //else
                    //{
                    //    GoToDestination(DestOptimized);
                    //}
                    //DestOptimized = null;
                }
            }

        }

        #endregion

        #region Food

        /// <summary>
        /// This method is called as soon as an ant sees an apple within its 360° 
        /// visual range. The parameter is the piece of fruit that the ant has spotted.
        /// Read more: "http://wiki.antme.net/en/API1:Spots(Fruit)"
        /// </summary>
        /// <param name="fruit">spotted fruit</param>
        public override void Spots(Fruit fruit)
        {
            //if (!(Destination is Bug))
            //    if (CurrentLoad == 0 && NeedsCarrier(fruit))
            //    {
            //        GoToDestination(fruit);
            //    }
        }

        /// <summary>
        /// This method is called as soon as an ant sees a mound of sugar in its 360° 
        /// visual range. The parameter is the mound of sugar that the ant has spotted.
        /// Read more: "http://wiki.antme.net/en/API1:Spots(Sugar)"
        /// </summary>
        /// <param name="sugar">spotted sugar</param>
        public override void Spots(Sugar sugar)
        {
            this.sugar = sugar;

            if (Destination == null && DestOptimized == null)
            {
                GoToDestination(sugar);
            }
        }

        /// <summary>
        /// If the ant’s destination is a piece of fruit, this method is called as soon 
        /// as the ant reaches its destination. It means that the ant is now near enough 
        /// to its destination/target to interact with it.
        /// Read more: "http://wiki.antme.net/en/API1:DestinationReached(Fruit)"
        /// </summary>
        /// <param name="fruit">reached fruit</param>
        public override void DestinationReached(Fruit fruit)
        {
            //if (NeedsCarrier(fruit))
            //{
            //    Take(fruit);
            //    GoToAnthill();
            //}
        }

        /// <summary>
        /// If the ant’s destination is a mound of sugar, this method is called as soon 
        /// as the ant has reached its destination. It means that the ant is now near 
        /// enough to its destination/target to interact with it.
        /// Read more: "http://wiki.antme.net/en/API1:DestinationReached(Sugar)"
        /// </summary>
        /// <param name="sugar">reached sugar</param>
        public override void DestinationReached(Sugar sugar)
        {
            Take(sugar);
            GoToAnthillOptimized();
        }

        #endregion

        #region Communication

        /// <summary>
        /// Friendly ants can detect markers left by other ants. This method is called 
        /// when an ant smells a friendly marker for the first time.
        /// Read more: "http://wiki.antme.net/en/API1:DetectedScentFriend(Marker)"
        /// </summary>
        /// <param name="marker">marker</param>
        public override void DetectedScentFriend(Marker marker)
        {
            //if (Destination == null && DestOptimized == null)
            //{
            //    GoToDestOptimized(marker);
            //}
        }

        /// <summary>
        /// Just as ants can see various types of food, they can also visually detect 
        /// other game elements. This method is called if the ant sees an ant from the 
        /// same colony.
        /// Read more: "http://wiki.antme.net/en/API1:SpotsFriend(Ant)"
        /// </summary>
        /// <param name="ant">spotted ant</param>
        public override void SpotsFriend(Ant ant)
        {
        }

        /// <summary>
        /// Just as ants can see various types of food, they can also visually detect 
        /// other game elements. This method is called if the ant detects an ant from a 
        /// friendly colony (an ant on the same team).
        /// Read more: "http://wiki.antme.net/en/API1:SpotsTeammate(Ant)"
        /// </summary>
        /// <param name="ant">spotted ant</param>
        public override void SpotsTeammate(Ant ant)
        {
        }

        #endregion

        #region Fight

        /// <summary>
        /// Just as ants can see various types of food, they can also visually detect 
        /// other game elements. This method is called if the ant detects an ant from an 
        /// enemy colony.
        /// Read more: "http://wiki.antme.net/en/API1:SpotsEnemy(Ant)"
        /// </summary>
        /// <param name="ant">spotted ant</param>
        public override void SpotsEnemy(Ant ant)
        {
        }

        /// <summary>
        /// Just as ants can see various types of food, they can also visually detect 
        /// other game elements. This method is called if the ant sees a bug.
        /// Read more: "http://wiki.antme.net/en/API1:SpotsEnemy(Bug)"
        /// </summary>
        /// <param name="bug">spotted bug</param>
        public override void SpotsEnemy(Bug bug)
        {
            //Drop();
            //if (FriendlyAntsInViewrange >= 2)
            //{
            //    Attack(bug);

            //}
            //else
            //{
            //    GoAwayFrom(bug);
            //}
        }

        /// <summary>
        /// Enemy creatures may actively attack the ant. This method is called if an 
        /// enemy ant attacks; the ant can then decide how to react.
        /// Read more: "http://wiki.antme.net/en/API1:UnderAttack(Ant)"
        /// </summary>
        /// <param name="ant">attacking ant</param>
        public override void UnderAttack(Ant ant)
        {
        }

        /// <summary>
        /// Enemy creatures may actively attack the ant. This method is called if a 
        /// bug attacks; the ant can decide how to react.
        /// Read more: "http://wiki.antme.net/en/API1:UnderAttack(Bug)"
        /// </summary>
        /// <param name="bug">attacking bug</param>
        public override void UnderAttack(Bug bug)
        {
        }

        #endregion

        private void GoToDestOptimized(Item item)
        {
            int distance = Coordinate.GetDistanceBetween(this, item);
            int angle = Coordinate.GetDistanceBetween(this, item);
            TurnByDegrees(angle);
            GoForward(distance);
            GoToAnthill();
            DestOptimized = item;
        }

        private void GoToAnthillOptimized()
        {
            GoToDestOptimized(anthill);
        }
    }
}