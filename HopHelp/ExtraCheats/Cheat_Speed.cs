namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Speed
    {
        [CheatMenu]
        public static void SetSpeed(string speed)
        {
            if (Generics.Player == null)
            {
                DevCheats.Log($"[SetSpeed] Player not found...");
                return;
            }

            if (float.TryParse(speed, out var setSpeed))
                Generics.Player.Motor.MoveDir = Generics.Player.transform.forward * setSpeed;
            else
                DevCheats.Log($"[SetSpeed] Speed value invalid...");
        }
    }
}
