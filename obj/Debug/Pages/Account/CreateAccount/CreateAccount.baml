   M S B A M L   `   `   `  ����G  CStrategySync, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:StrategySync.Pages.Account.CreateAccount(StrategySync.Pages.Account.CreateAccount  5   	   <    6StrategySync.Pages.Account.CreateAccount.CreateAccount   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Hlocal6clr-namespace:StrategySync.Pages.Account.CreateAccount     5   	     � Title$  CreateAccount��5   	   $	��300��6   $	��300��6,   .��5	      �    
GridStyle1#��[  6    � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Create Account��6   $��20(�6*   $
��Bold"�68   $��Center=�6J   $��0��5      $��2��6!   $��0,0,0,20q�65     TextBlockStyle1#��[ 6G   �� 5   
   $��	Username:��6   $��1��6%   $��0��62   $��Center=�6B   $��-5,0,5,0q�6]    �� TextAlignment$ Right=�6o     TextBlockStyle2#��[ 6�   �� 5   
   -   5        $��UsernameTextBox��5   @   $��1��6   $��1��6   $��0,5,5,5q�6/     TextBoxStyle1#��[ 6�   ��6W   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��Email:��6   $��2��6"   $��0��6/   $��Center=�6?   $��-5,0,5,0q�6Z   $ Right=�6l   #��[ 6�   �� 5   
   -   5        $��EmailTextBox��5   @   $��2��6   $��1��6   $��0,5,5,5q�6/   #��[ 6�   ��6T   �� *Email+$ PropertyChanged=�6�   �� 5!   
   $��	Password:��6   $��3��6%   $��0��62   $��Center=�6B   $��-5,0,5,0q�6]   $ Right=�6o   #��[ 6�   G� 5"   
   -   5        $��PasswordBox��5"   D   $��3��6   $��1��6#   $��0,5,5,5q�63     PassBoxStyle1#��[ 6W   �� 5$   
   $��Confirm Password:��6   $��4��6-   $��0��6:   $��Center=�6J   $��0,5,5,5q�6e   $ Right=�6v   #��[ 6�   G� 5%   
   -   5        $��ConfirmPasswordBox��5%   D   $��4��6   $��1��6#   $��0,5,5,5q�63   #��[ 6^   �� 5'   
   -   5        $��Create Account��5'      $��5��6*   $��2��67   $��Center=�6K   $	��150��6h   $��30��6t   $��0,20,0,0q�5(        ButtonStyle#��[ 6E   5)      5*      