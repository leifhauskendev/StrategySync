   M S B A M L   `   `   `  ����G  CStrategySync, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:StrategySync.Pages.StratagiesStrategySync.Pages.Stratagies  5   	   2    ,StrategySync.Pages.Stratagies.SelectStrategy   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:StrategySync.Pages.Stratagies     5   	     � Title$  Select Strategy��5   	   $	��600��6!   $
��1000��6.   .��5	      �    
GridStyle1#��[  6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��
Strategies��6!   $
��Left=�63   $��40(�6N   $��10q�6\     TextBlockStyle1#��[ 6h   �� 5   
   -   5        $��0��5      $��Create Strategy��6   $��Right=�68   $��30��6T   $��5q�6`   $��10q�6l   $��Center=�6x     ButtonStyle#��[ 6�   �� 5   
   $��1��6   $��No data.��6!   $��15(�61   $��Center=�6?   y�6\   �� *NoDataTextVisiblity+�� 5   
   $��1��6   $��
Loading...��6!   $��15(�63   $��Center=�6A   y�6^   �� *LoadingTextVisiblity+�� 5   
   -   5         ��Name$ StrategyListView��5      $��1��6-     ListViewStyle#��[ 6�   y�6:   �� *StrategyListVisiblity+��6g   �� *Source+��5      �� 5      .R�5      � -   5         � ShowGridLines .   � ColumnDefinitions 5      �� 5      $
��Auto �6/   �� 5      $��5 �6/   �� 5      $	��150 �6/   �� 5      $
��Auto �6/   �� 5      $��5 �6/   �� 5      $	��150 �6/   �� 5       $
��Auto �6/   �� 5!      $��5 �6/   �� 5"      $	��150 �6/   �� 5#      $
��Auto �6/   �� 5$      $��5 �6/   �� 5%      $	��150 �6/   �� 5&      $��5 �6/   �� 5'      $
��Auto �6/   .+�5*      �� $��0��6$   $��Name: ��64   �� 5+      $��2��6$   ��64   �� *Name+�� 5-      $��3��6$   $��Map: ��64   �� 5.      $��5��6$   ��64   �� *	MapName+�� 50      $��6��6$   $��Last Opened: ��64   �� 51      $��8��6$   ��64   �� *
LastOpened+�� 53      $��9��6$   $��Checked Out: ��64   �� 54      $��11��6$   ��65   �� *IsCheckedOut+�� 56       ��Name$ DeleteButton��6!   -   5        $��13��56   7   $��15��6H   $��15��6S    � ToolTip$ Delete this strategy��6z   
������ 
������ y�6�   �� *	IsOwner+.��57      �� $#��/Media/Icons/TrashbinIcon.png��6$   $��Uniform=�6K   58      59      5:      5<      5=      5>      