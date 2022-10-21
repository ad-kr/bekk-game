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

            DialogueBox.Talk(ApproachNote, "Er..", "Er dette konsulentene våre?");
        }

        private async void ApproachNote()
        {
            PlayerWalkTowardsState walk = new(new Vector2(328f, -131f));
            Player.Instance.State = walk;

            await walk.Finished;

            await Game.Wait(1f);
            DialogueBox.Talk(ShowStations, new string[] {
                "Hmm, noen la igjen et notat...",
                "...fra Olav?",
                "...Leser du dette, frykter jeg at det allerede er for sent...",
                "Det var i denne bygningen at Bekk pleide å holde til.",
                "Det var en tid med kontorer fulle av dyktige konsulenter, uvitne om hvilken fremtid som ventet dem.",
                "De bygget løsninger som forbedret samfunnet, skapte verdi for kundene sine, og bygget prisvinnende produkter ingen hadde sett maken til.",
                "Men det var aldri nok. Kundene ville ha mer...",
                "Mer smidighet",
                "...større logo...",
                "...og blockchain teknologi.",
                "Vi oppsøkte kunstig intelligens.",
                "Først Copilot...",
                "...så Dall.e",
                "Alt gikk så sømløst i starten, kundene var storfornøyde, pengene dalte ned.",
                "Men det viste seg å bare være begynnelsen.",
                "Automatiserte kodemaskiner, forretnings-androider, ja til og med AI-drevne designere. Vi trengte bare å lene oss tilbake og se at arbeidet ble gjort.",
                "Men maskinene ble for smarte. De begynte å se på konsulentene som en hindring. En hindring for deres effektivitet, og ikke minst, en hindring for veksten til Bekk.",
                "Enten det, eller så var det å kvitte seg med oss bare planen til å begynne med.",
                "Jeg skriver dette som et siste forsøk på å redde det som gjenstår.",
                "Du som leser dette er Bekk sitt aller siste håp.",
                "Til høyre for for deg skal det stå tre kryokamre",
                "I disse har jeg fryst ned de dyktigste konsulentene fra teknologi, design og BMC",
                "Hvis du klarer å frigjøre dem, har vi kanskje fortsatt en sjanse for en ny start",
                "For å gjøre dette må du først deaktivere kontroll-stasjonene."
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
                "Klarer du dette, må du deaktivere det sentrale systemet. Du finner det i Hovedøya.",
            });
        }

        private async void ReturnToDialogue()
        {
            await Game.Wait(1f);

            Camera.AttachTo(Player.Instance);

            await Game.Wait(2f);

            DialogueBox.Talk(GiveWeapon, new string[] {
                "Dette kommer ikke til å være en enkel oppgave. Du er advart.",
                "Jeg la igjen et brekkjern for å hjelpe deg, du kommer til å trenge det.",
            });
        }

        private async void GiveWeapon()
        {
            Player.Instance.EquippedWeapon = new Crowbar();
            QueueFree();

            await Game.Wait(1f);

            DialogueBox.Talk(SetNewObjective, "Husk, konsulentenes fremtid ligger i dine hender. - Olav");
        }

        private void SetNewObjective()
        {
            World.Instance.Objectives.Objective = new StationsObjective();
        }
    }
}