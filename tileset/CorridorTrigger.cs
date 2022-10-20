using Godot;
using System;

namespace ADKR.Game
{
    public partial class CorridorTrigger : Trigger
    {
        public override async void Execute()
        {
            PlayerWalkTowardsState walk = new(new Vector2(352f, -48f));
            Player.Instance.State = walk;

            await walk.Finished;

            walk = new(new Vector2(352f, -110f));
            Player.Instance.State = walk;

            await walk.Finished;

            await Game.Wait(1f);

            DialogueBox.Talk(ApproachNote, "???", "Er..", "Er dette konsulentene våre?", "Hva skjedde med dem?");
        }

        private async void ApproachNote()
        {
            PlayerWalkTowardsState walk = new(new Vector2(328f, -131f));
            Player.Instance.State = walk;

            await walk.Finished;

            await Game.Wait(1f);
            DialogueBox.Talk(ShowStations, new string[] {
                "Hmm, en notat...",
                "...fra Olav?",
                "...Hvis du leser dette, er det sannsynligvis for sent...",
                "En gang i tiden var denne bygningen full av konsulenter.",
                "Vi skapte verdi for kundene og samfunnet.",
                "...men kundene ville ha mer.",
                "Mer smidighet...",
                "...større logo...",
                "...og blockchain teknologi.",
                "Vi prøvde å få hjelp av kunnstig intelligens.",
                "Først Copilot...",
                "...så Dall.e",
                "Så tok AI over...",
                "Konsulentene var ikke lenger effektive nok for AI-en.",
                "De har utviklet sine egne robotter som skulle gjøre jobben for oss... Skuret og Bekk er nå tapt for alltid.",
                "Men... Kanksje det ikke er for sent allikavel?",
                "Kanskje du kan beseire den onde AI-en?",
                "Ser du disse kryokamrene?",
                "I disse har jeg lukket våre flinkeste konsulenter fra teknologi, design og BMC.",
                "Frigjør dem. Sammen kan dere bygge Bekk på nytt.",
                "...",
                "For å gjøre dette må du først deaktivere alle kontroll-stasjonene til AI-en.",
            });
        }

        private async void ShowStations()
        {
            await Game.Wait(1f);
            string[] strings = new[]{
                $"Du kan finne stasjonene i {ControlStation.Stations[0].Name}",
                $"{ControlStation.Stations[1].Name}...",
                $"ved {ControlStation.Stations[2].Name}...",
                $"...og den siste i {ControlStation.Stations[3].Name}.",
            };

            NextStation(strings, 0);
        }

        private async void NextStation(string[] strings, int index)
        {
            if (index == strings.Length)
            {
                LookAtMain();
                return;
            }

            Camera.AttachTo(ControlStation.Stations[index]);

            await Game.Wait(2f);

            DialogueBox.Talk(() =>
            {
                NextStation(strings, index + 1);
            }, strings[index]);

        }

        private async void LookAtMain()
        {
            await Game.Wait(1f);

            Camera.AttachTo(Boss.Instance);

            await Game.Wait(2f);

            DialogueBox.Talk(ReturnToDialogue, new string[] {
                "Når du er ferdig med dette kan du deaktivere det sentrale systemet i Hovedøya.",
            });
        }

        private async void ReturnToDialogue()
        {
            await Game.Wait(1f);

            Camera.AttachTo(Player.Instance);

            await Game.Wait(2f);

            DialogueBox.Talk(GiveWeapon, new string[] {
                "Men du må passe på. Skuret er fyllt av roboter som kommer til å prøve å angripe deg.",
                "Ta dette for å kjempe mot robotene.",
            });
        }

        private async void GiveWeapon()
        {
            Player.Instance.EquippedWeapon = new Crowbar();
            QueueFree();

            await Game.Wait(1f);

            DialogueBox.Talk(SetNewObjective, "Lykke til. - Olav");
        }

        private void SetNewObjective()
        {
            World.Instance.Objectives.Objective = new StationsObjective();
        }
    }
}