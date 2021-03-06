﻿namespace Aimtec.SDK.Menu.Theme.Default
{
    using System;
    using System.Drawing;

    using Aimtec.SDK.Menu.Components;

    using Rectangle = Aimtec.Rectangle;

    internal class DefaultMenuSliderBool : IRenderManager<MenuSliderBool, DefaultMenuTheme>
    {
        #region Constructors and Destructors

        public DefaultMenuSliderBool(MenuSliderBool sliderBool, DefaultMenuTheme defaultMenuTheme)
        {
            this.Component = sliderBool;
            this.Theme = defaultMenuTheme;
        }

        #endregion

        #region Public Properties

        public MenuSliderBool Component { get; }

        public DefaultMenuTheme Theme { get; }

        #endregion

        #region Public Methods and Operators

        public void Render(Vector2 pos)
        {
            var width = this.Component.Parent.Width;

            var height = MenuManager.MaxHeightItem + this.Theme.BonusMenuHeight;

            var beforeSliderWidth = (float) (this.Component.Value - this.Component.MinValue)
                / (this.Component.MaxValue - this.Component.MinValue)
                * (width - this.Theme.IndicatorWidth - this.Theme.LineWidth * 2);

            var afterSliderWidth = width - this.Theme.IndicatorWidth - beforeSliderWidth - this.Theme.LineWidth;

            this.Theme.DrawMenuItemBorder(pos, width);

            var position = pos + this.Theme.LineWidth;

            this.Theme.DrawMenuItemBox(position, width);

            var displayNamePosition = new Aimtec.Rectangle((int)position.X + this.Theme.TextSpacing, (int)position.Y, (int)(position.X + width), (int)(position.Y + height));

            // Draw light bar before the slider line
            Aimtec.Render.Rectangle(
                position,
                beforeSliderWidth,
                height * 0.95f,
                Color.FromArgb(14, 59, 73));

            var beforeSliderPos = position + new Vector2(beforeSliderWidth, 0);

            Aimtec.Render.Line(
                beforeSliderPos,
                beforeSliderPos + new Vector2(0, height),
                this.Theme.LineWidth,
                false,
                Color.FromArgb(82, 83, 57));

            var afterSliderPos = beforeSliderPos + new Vector2(this.Theme.LineWidth, 0);

            Aimtec.Render.Rectangle(
                afterSliderPos,
                afterSliderWidth - this.Theme.LineWidth * 2,
                height * 0.95f,
                Color.FromArgb(16, 26, 29));


            // Render indicator box outline
            Aimtec.Render.Line(
                pos.X + width - this.Theme.IndicatorWidth - this.Theme.LineWidth,
                pos.Y,
                pos.X + width - this.Theme.IndicatorWidth - this.Theme.LineWidth,
                pos.Y + height,
                Color.FromArgb(82, 83, 57));

            // Draw indicator box
            var boolColor = this.Component.Enabled ? Color.FromArgb(39, 96, 17) : Color.FromArgb(85, 25, 15);

            var indBoxPosition = position + new Vector2(width - this.Theme.IndicatorWidth - this.Theme.LineWidth, 0);

            Aimtec.Render.Rectangle(
                indBoxPosition,
                this.Theme.IndicatorWidth,
                height - this.Theme.LineWidth,
                boolColor);

            Aimtec.Render.Text(this.Component.Value.ToString(),
                new Rectangle((int)(pos.X), (int)pos.Y + this.Theme.LineWidth, (int)(pos.X + width - this.Theme.TextSpacing - this.Theme.IndicatorWidth), (int)(pos.Y + height)),
                RenderTextFlags.VerticalCenter | RenderTextFlags.HorizontalRight, this.Theme.TextColor);

            var centerArrowBox = new Aimtec.Rectangle((int)indBoxPosition.X, (int)indBoxPosition.Y, (int) (indBoxPosition.X + this.Theme.IndicatorWidth), (int)(indBoxPosition.Y + height));

            Aimtec.Render.Text(this.Component.Enabled ? "ON" : "OFF",
                centerArrowBox,
                RenderTextFlags.HorizontalCenter | RenderTextFlags.VerticalCenter, Color.White);

            Aimtec.Render.Text(this.Component.DisplayName + (!string.IsNullOrEmpty(this.Component.ToolTip) ? " [?]" : ""),
                displayNamePosition,
                RenderTextFlags.VerticalCenter, this.Theme.TextColor);
        }

        #endregion
    }
}