   M S B A M L   `   `   `  ����G  CStrategySync, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:StrategySync.Pages.StratagiesStrategySync.Pages.Stratagies  5   	   a5clr-namespace:StrategySync.Enumerations.StrategyEnums'StrategySync.Enumerations.StrategyEnums  5   	   Q-clr-namespace:StrategySync.Classes.ConvertersStrategySync.Classes.Converters  5   	   8    2StrategySync.Pages.Stratagies.CreateStrategyWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:StrategySync.Pages.Stratagies     5   	   Genums5clr-namespace:StrategySync.Enumerations.StrategyEnums     5   	   C	converter-clr-namespace:StrategySync.Classes.Converters     5   	     � Title$  Create New Strategy��5
   	   $	��200��6%   $	��200��62    � 	Resources 5      9   3StrategySync.Classes.Converters.EnumToListConverter   EnumToListConverter8   2StrategySync.Classes.Converters.EnumToIntConverter  EnumToIntConverter��%:   &	        &	        5   
   6(     5   
   6'   5      .��5      �   
GridStyle1#��[ 6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��5 �6   �� 5      $��* �6   .+�5   
   �� $��2��6   $��0��6!   $
��Name��61   $��2��6=   $��
0,10,5,110q�6Q     TextBlockStyle1#��[ 6e   �� 5   
   $��2��6   $��2��6   $��
0,10,5,150q�6L   ��6/   �� *Source.Name+6L   �� 5!   
   $��2��6   $��0��6!   $	��Map��61   $��2��6<   $��	2,45,2,77q�6P   #��[ 6c   �� 5"   
   -   5        $��MapComboBox��5"      $��2��6&   $��2��63   $��
0,45,5,118q�6.  ��6C   ��  �� Source1   +StrategySync.Enumerations.StrategyEnums.Map"   �� 	Converter# [   �� SelectedValue 6�   �� *
Source.Map+ �� Mode$ TwoWay=�# [ 6.  �� 5$   
   -   5        $��2��5$      $��1��6   $��Create��6.   $��Center=�6?   $��	38,0,60,0q�6Z     ButtonStyle#��[ 6�   $��2��6�   5%      5&      