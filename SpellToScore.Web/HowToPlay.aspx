<%@ Page Title="How To Play" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HowToPlay.aspx.cs" Inherits="SpellToScore.Web.HowToPlay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>How To Play</h2>
    <p>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
    </p>
    <h3>Aim</h3>
    <p>
        The aim of level one is to shoot letters to spell out colours. Words spelled are
        scored on the letters it&#39;s made up of, so more uncommon letters as well as longer
        words will score more highly. In level two you must hit as many even numbers as
        possible, but be careful not to hit odd numbers as you will lose points! There is
        a time limit, and you will be awarded a bonus for hitting parrots, and a penalty
        for hitting seagulls.
    </p>
    <h3>Controls</h3>
    <p>
        <img alt="Controls" style="width: 520px; height: 212px;" src="Styles/how_to_play_controls.png" />
    </p>
    <h3>Letter Scoring</h3>
    <p>
        This is how many points each letter will be awarded in level one:
    </p>
    <p>
        <strong>1 point:</strong> A, E, I, L, N, O, R, S, T, U<br />
        <strong>2 points:</strong> D, G<br />
        <strong>3 points:</strong> B, C, M, P<br />
        <strong>4 points:</strong> F, H, V, W, Y<br />
        <strong>5 points:</strong> K<br />
        <strong>8 points:</strong> J, X<br />
        <strong>10 points:</strong> Q, Z
    </p>
    <h3>List of Words</h3>
    <p>
        The following is the complete list of colours that will be accepted as correct words
        in the game:
    </p>
    <p>
        Alizarin, amaranth, amber, amethyst, apricot, aqua, aquamarine, asparagus, auburn,
        azure, beige, bistre, black, blossom, blue, brass, bronze, brown, buff, burgundy,
        camouflage, cardinal, carmine, carrot, celadon, cerise, cerulean, champagne, charcoal,
        chartreuse, cherry, chestnut, chocolate, cinnabar, cinnamon, cobalt, copper, coral,
        corn, cornflower, cream, crimson, cyan, dandelion, denim, ecru, emerald, eggplant,
        fern, firebrick, flax, forest, fuchsia, gold, goldenrod, green, grey , harlequin,
        heliotrope, indigo, ivory, jade, khaki, lavender, lemon, lilac, lime, linen, magenta,
        magnolia, malachite, maroon, mauve, midnight, mint, moss, mustard, myrtle, navy,
        ochre, olive, olivine, orange, orchid, papaya, peach, pear, periwinkle, persimmon,
        pink, platinum, plum, puce, pumpkin, purple, razzmatazz, red, rose, ruby, russet,
        rust, saffron, salmon, sangria, sapphire, scarlet, seashell, sepia, silver, tan,
        tangerine, taupe, teal, thistle, tomato, turquoise, ultramarine, vermilion, violet,
        viridian, wheat, white, wisteria, yellow, zucchini.
    </p>
</asp:Content>