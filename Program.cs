using System;

namespace Blackjack
{
    class Program
    {
        static int[] CardValue = new int[52]
            {2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10,11,
                2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11,
                2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11,
                2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
        static string[] CardName = new string[52]
            {"Clover 2","Clover 3", "Clover 4", "Clover 5", "Clover 6", "Clover 7", "Clover 8", "Clover 9", "Clover 10", "Clover Jack", "Clover Queen", "Clover King", "Clover Ace",
                "Diamond 2","Diamond 3", "Diamond 4", "Diamond 5", "Diamond 6", "Diamond 7", "Diamond 8", "Diamond 9", "Diamond 10", "Diamond Jack", "Diamond Queen", "Diamond King", "Diamond Ace",
                "Heart 2","Heart 3", "Heart 4", "Heart 5", "Heart 6", "Heart 7", "Heart 8", "Heart 9", "Heart 10", "Heart Jack", "Heart Queen", "Heart King", "Heart Ace",
                "Spade 2","Spade 3", "Spade 4", "Spade 5", "Spade 6", "Spade 7", "Spade 8", "Spade 9", "Spade 10", "Spade Jack", "Spade Queen", "Spade King", "Spade Ace"
            };
        static Random rnd = new Random();
        static int[] DealtCards = new int[52];
        static int[] DealersCards = new int[52];
        static int[] PlayersCards = new int[52];
        static void DealToDealer(int[] block)
        {
            int x = 0;
            bool card = false;
            while (card == false)
            {
                int deal = rnd.Next(0, 51);
                for (int i = 0; i < block.Length; i++)
                {
                    if (i == deal && block[i] == 0)
                    {
                        block[i] = 1;
                        DealersCards[i] = 1;
                        x = i;
                        card = true;
                    }
                }
            }
        }
        static void DealToPlayes(int[] block)
        {
            int x = 0;
            bool card = false;
            while (card == false)
            {
                int deal = rnd.Next(0, 51);
                for (int i = 0; i < block.Length; i++)
                {
                    if (i == deal && block[i] == 0)
                    {
                        block[i] = 1;
                        PlayersCards[i] = 1;
                        x = i;
                        card = true;
                    }
                }
            }
        }
        static void AceChange(int[] block)
        {
            for (int i = 0; i < block.Length; i++)
            {
                if (block[i] == 1 && CardValue[i] == 11)
                {
                    Console.WriteLine("Do you want {0} to be worth only one point? (You can do this another time)", CardName[i]);
                    Console.WriteLine("If yes, press enter.");
                    Console.WriteLine("If not, press 'n' and then enter.");
                    string ace = Console.ReadLine();
                    if (ace == "")
                    {
                        CardValue[i] = 1;
                    }
                }
            }
        }
        static void CardPrint(int[] block)
        {
            for (int i = 0; i < block.Length; i++)
            {
                if (block[i] == 1)
                {
                    Console.WriteLine(CardName[i]);
                }
            }
        }
        static int Totalize(int[] block)
        {
            int x = 0;
            for (int i = 0; i < block.Length; i++)
            {
                if (block[i] == 1)
                {
                    x = x + CardValue[i];
                }
            }
            return x;
        }
        static int DealerRound(int score)
        {
            int x = score;
            if (x <= 16)
            {
                while (x <= 16)
                {
                    DealToDealer(DealtCards);
                    x = Totalize(DealersCards);
                }
                if (x > 21)
                {
                    for (int i = 0; i < DealersCards.Length; i++)
                    {
                        if (DealersCards[i] == 1 && CardValue[i] == 11)
                        {
                            {
                                CardValue[i] = 1;
                                x = Totalize(DealersCards);
                                DealerRound(x);
                                break;
                            }
                        }
                    }
                }
            }
            return x;

        }
        static void Result(int dealer, int player)
        {
            Console.WriteLine("The result:");
            Console.WriteLine("The dealer's cards:");
            CardPrint(DealersCards);
            Console.WriteLine("\n");
            Console.WriteLine("Your cards:");
            CardPrint(PlayersCards);
            Console.WriteLine("\n");
            if (dealer > 21)
            {
                if (player < 21)
                {
                    Console.WriteLine("You won!");
                }
                else if (player == 21)
                {
                    Console.WriteLine("You won, with Black Jack!");
                }
                else
                {
                    Console.WriteLine("You both have more points than 21, neither of you won!");
                }
            }
            else if (dealer == 21)
            {
                if (player == 21)
                {
                    Console.WriteLine("You both have Black Jack!");
                }
                else
                {
                    Console.WriteLine("The dealer has Black Jack, you didn't stand a chance!");
                }
            }
            else
            {
                if (player == 21)
                {
                    Console.WriteLine("You have Black Jack, you won!");
                }
                else if (player > 21)
                {
                    Console.WriteLine("You got too many points, unfortunately you lost!");
                }
                else
                {
                    if (player > dealer)
                    {
                        Console.WriteLine("You got more points, you won!");
                    }
                    else if (player == dealer)
                    {
                        Console.WriteLine("You got equal points, so neither of you won!");
                    }
                    else
                    {
                        Console.WriteLine("Unfortunately you got less points, you lost!");
                    }
                }
            }

        }
        static void Main(string[] args)
        {
            int game = 0;
            int playerScore = 0;
            int dealerScore = 0;
            while (game == 0)
            {
                DealToDealer(DealtCards);
                Console.WriteLine("The dealer's first card:");
                CardPrint(DealersCards);
                Console.WriteLine("\n");
                DealToDealer(DealtCards);
                DealToPlayes(DealtCards);
                DealToPlayes(DealtCards);
                Console.WriteLine("Your cards:");
                CardPrint(PlayersCards);
                Console.WriteLine("\n");
                playerScore = Totalize(PlayersCards);
                dealerScore = Totalize(DealersCards);
                if (playerScore == 21)
                {
                    dealerScore = DealerRound(dealerScore);
                    Result(dealerScore, playerScore);
                    game = 1;
                }
                else if (playerScore == 22)
                {
                    AceChange(PlayersCards);
                    playerScore = Totalize(PlayersCards);
                }
                while (true)
                {
                    Console.WriteLine("Do you want more card?");
                    Console.WriteLine("If not, press enter.");
                    Console.WriteLine("If you want, press anything else, then enter.");
                    string askCard = Console.ReadLine();
                    if (askCard == "")
                    {
                        playerScore = Totalize(PlayersCards);
                        dealerScore = DealerRound(dealerScore);
                        Result(dealerScore, playerScore);
                        break; ;
                    }
                    DealToPlayes(DealtCards);
                    Console.WriteLine("Your cards:");
                    CardPrint(PlayersCards);
                    AceChange(PlayersCards);
                    playerScore = Totalize(PlayersCards);
                    if (playerScore > 21)
                    {
                        dealerScore = DealerRound(dealerScore);
                        Result(dealerScore, playerScore);
                        break;
                    }
                    else if (playerScore == 21)
                    {
                        dealerScore = DealerRound(dealerScore);
                        Result(dealerScore, playerScore);
                        break;
                    }
                }
                game = 1;
            }
            Console.WriteLine("End of the game.");
            Console.ReadKey();
        }

    }
}

