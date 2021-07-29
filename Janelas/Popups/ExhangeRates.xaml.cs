﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TrueMiningDesktop.Janelas.Popups
{
    /// <summary>
    /// Lógica interna para Calculator.xaml
    /// </summary>
    public partial class ExchangeRates : Window
    {
        public ExchangeRates(decimal exchangeRatePontosToMiningCoin)
        {
            InitializeComponent();
            new System.Threading.Tasks.Task(() =>
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    loadingVisualElement.Visibility = Visibility.Visible;
                    AllContent.Visibility = Visibility.Hidden;

                    if (Janelas.Pages.Dashboard.loadingVisualElement.Visibility == Visibility.Visible)
                    {
                        Close();

                        MessageBox.Show("Wait for Dashboard load first"); return;
                    }
                    else
                    {
                        CoinName = User.Settings.User.Payment_Coin;

                        BTCToCoinRate = Decimal.Round(BTCToBTCRate / TrueMiningDesktop.Coinpaprika.CoinpaprikaData.COIN_BTC_ohlcv.Last().close / 2);
                        BTCToBTCRate = 1;
                        BTCToUSDRate = Decimal.Round(PoolAPI.BitcoinPrice.BTCUSD, 2);

                        PointToCoinRate = Decimal.Round(exchangeRatePontosToMiningCoin, 5);
                        PointToBTCRate = Decimal.Round(BTCToBTCRate / TrueMiningDesktop.Coinpaprika.CoinpaprikaData.COIN_BTC_ohlcv.Last().close * exchangeRatePontosToMiningCoin / BTCToBTCRate, 8);
                        PointToUSDRate = Decimal.Round(BTCToBTCRate / TrueMiningDesktop.Coinpaprika.CoinpaprikaData.COIN_BTC_ohlcv.Last().close * exchangeRatePontosToMiningCoin / BTCToBTCRate * BTCToUSDRate, 5);

                        CoinToCoinRate = 1;
                        CoinToPointRate = Decimal.Round(CoinToCoinRate / PointToCoinRate, 5);
                        CoinToBTCRate = Decimal.Round(BTCToBTCRate / TrueMiningDesktop.Coinpaprika.CoinpaprikaData.COIN_BTC_ohlcv.Last().close / BTCToBTCRate, 8);
                        CoinToUSDRate = Decimal.Round(BTCToBTCRate / TrueMiningDesktop.Coinpaprika.CoinpaprikaData.COIN_BTC_ohlcv.Last().close / BTCToBTCRate * BTCToUSDRate, 5);

                        loadingVisualElement.Visibility = Visibility.Hidden;
                        AllContent.Visibility = Visibility.Visible;

                        DataContext = null;
                        DataContext = this;
                    }
                });
            }).Start();
        }

        public string CoinName { get; set; }

        public decimal PointToCoinRate { get; set; } = 1;
        public decimal PointToBTCRate { get; set; } = 1;
        public decimal PointToUSDRate { get; set; } = 1;

        public decimal CoinToCoinRate { get; set; } = 1;
        public decimal CoinToPointRate { get; set; } = 1;
        public decimal CoinToBTCRate { get; set; } = 1;
        public decimal CoinToUSDRate { get; set; } = 1;

        public decimal BTCToCoinRate { get; set; } = 1;
        public decimal BTCToBTCRate { get; set; } = 1;
        public decimal BTCToUSDRate { get; set; } = 1;

        private void CloseButton_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }

        private static bool clicado;
        private Point lm;

        public void Down(object sender, MouseButtonEventArgs e)
        {
            clicado = true;

            lm.X = System.Windows.Forms.Control.MousePosition.X;
            lm.Y = System.Windows.Forms.Control.MousePosition.Y;
            lm.X = Convert.ToInt16(Left) - lm.X;
            lm.Y = Convert.ToInt16(Top) - lm.Y;
        }

        public void Move(object sender, MouseEventArgs e)
        {
            if (clicado && e.LeftButton == MouseButtonState.Pressed)
            {
                Left = (System.Windows.Forms.Control.MousePosition.X + lm.X);
                Top = (System.Windows.Forms.Control.MousePosition.Y + lm.Y);
            }
            else { clicado = false; }
        }

        public void Up(object sender, MouseButtonEventArgs e)
        {
            clicado = false;
        }
    }
}