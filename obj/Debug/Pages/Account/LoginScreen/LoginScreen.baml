   M S B A M L   `   `   `  ����G  CStrategySync, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:StrategySyncStrategySync  5      ,    &StrategySync.Pages.Account.LoginScreen   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:StrategySync     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        N� Title$  LoginScreen��5      .��5      �  � ShowGridLines .     
GridStyle1#��[  6!    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��300 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��1��6!   $��Center=�61   $��Center=�6L   $��50(�6i   $��StrategySync��6w     TextBlockStyle1#��[ 6�   �� 5   
   -   5         ��Name$ UsernameTextBox��5   0   $��2��6   $��1��6   $��0,5,0,5q�6w     TextBoxStyle1#��[ 6�   ��6I   �� *Source.Username+ �� Mode$ TwoWay=�6�   G� 5!   
   -   5        $��PasswordBox��5!   a   $��3��6   $��1��6#     PassBoxStyle1#��[ 6t   �� 5"   
   -   5        $��LoginButton��5"   >   $��4��6   $��1��6   $��Login��6.   $��0,5,0,5q�6k     ButtonStyle#��[ 6|   �� 5#   
   -   5        $��CreateAccountButton��5#   G   $��5��6   $��1��6   $��Create Account��6.   $��0,5,0,5q�6�   #��[ 6�   �� 5$   
   -   5        $��ForgotPasswordButton��5$   H   $��6��6   $��1��6   $��Forgot Password��6.   $��0,5,0,5q�6�   #��[ 6�   5%      5&      