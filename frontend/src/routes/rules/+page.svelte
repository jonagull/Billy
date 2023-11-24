<script lang="ts">
    let toggle: boolean = true;

    function handleToggle() {
        toggle = !toggle;
    }
</script>

<h1>Rules</h1>

{#if toggle}
    <div class="p-4">
        <p class="mb-4">
            <strong> In the game of 8-ball pool </strong>, players aim to pocket
            all the balls in their designated group (solids or stripes) while
            strategically positioning the cue ball. The player must pocket all
            their designated balls first before attempting to sink the 8 ball to
            win the game. However, players must be careful not to pocket the 8
            ball prematurely or commit fouls, which can result in penalties. The
            game requires a combination of skill, strategy, and careful shot
            selection to outsmart the opponent and secure victory.
        </p>

        <hr class="mb-4" />

        <p class="mb-4">
            In 8-ball pool, a legal shot is one that meets certain requirements
            and does not result in a foul. The specific rules may vary depending
            on the set of rules being followed, but here are some common
            guidelines for a legal shot in 8-ball pool:
        </p>

        <ul class="list-disc pl-8 mb-4">
            <li>
                <strong>Cue Ball Contact:</strong> The cue ball must strike one of
                the balls in the player's designated group (either solids or stripes)
                first, and a ball from that group must be pocketed or hit a rail
                after the contact.
            </li>
            <li>
                <strong>Object Ball Contact:</strong> After striking a ball from
                the player's designated group, the cue ball may also contact and
                pocket balls from the opposing group (opponent's balls), but it is
                not required.
            </li>
            <li>
                <strong>Ball Pocketed:</strong> At least one ball must be legally
                pocketed on the shot. If a player pockets a ball from their designated
                group, they continue their turn; if a ball from the opponent's group
                is pocketed, it becomes the opponent's turn.
            </li>
            <li>
                <strong>No Fouls:</strong> The shot must not result in a foul, which
                can include things like scratching (cue ball being pocketed), failing
                to hit any ball, hitting the cue ball off the table, or violating
                other specific rules.
            </li>
        </ul>

        <p class="mb-4">
            It's important to note that different rule sets may have additional
            variations and requirements for a legal shot, such as whether the 8
            ball can be pocketed early or the requirement to call shots.
            Therefore, it's always recommended to clarify and agree upon the
            specific rules being followed before playing a game of 8-ball pool.
        </p>

        <hr class="mb-4" />

        <p class="mb-4">
            In the game of 8-ball pool,<strong class="eightball">
                "ball in hand" refers to a situation where a player has the
                freedom to place the cue ball anywhere on the table before
                taking their shot.
            </strong>However, the specific rules regarding where the player can
            place the cue ball after a "ball in hand" situation may vary
            depending on the rule set being followed.
        </p>

        <p class="mb-4">
            In many rule sets, including popular variations of 8-ball pool such
            as BCA (Billiard Congress of America) and WPA (World Pool-Billiard
            Association) rules, when a player has "ball in hand," they can place
            the cue ball anywhere on the table, as long as it is not touching
            any other ball. There is typically no requirement for the cue ball
            to be played from a specific area such as the kitchen (also known as
            the "cue ball in the kitchen" rule).
        </p>

        <p class="mb-4">
            <strong> The kitchen is a term </strong> used in some other cue sports
            games, such as snooker, to refer to a specific designated area of the
            table where the cue ball must be played from after certain fouls or other
            specific situations. However, in the context of 8-ball pool, the concept
            of the kitchen is not commonly used for positioning the cue ball after
            a "ball in hand" situation.
        </p>

        <p>
            It's always a good idea to clarify and agree upon the specific rules
            being followed with your opponent before playing a game of 8-ball
            pool to ensure a fair and enjoyable playing experience.
        </p>
    </div>
{:else}
    <div class="p-4">
        <p class="mb-4">
            <strong>I spillet 8-ball pool</strong> sikter spillerne på å lomme alle
            ballene i sin angitte gruppe (hele eller striper) samtidig som de strategisk
            plasserer køballen. Spilleren må lomme alle sine angitte baller før de
            prøver å senke 8-ball for å vinne spillet. Spillerne må imidlertid være
            forsiktige så de ikke lommer 8-ball for tidlig eller begår feil, noe
            som kan resultere i straff. Spillet krever en kombinasjon av ferdigheter,
            strategi og nøye valg av slag for å lure motstanderen og sikre seier.
        </p>

        <hr class="mb-4" />

        <p class="mb-4">
            I 8-ball pool er et lovlig slag et slag som oppfyller visse krav og
            ikke resulterer i en feil. De spesifikke reglene kan variere
            avhengig av hvilket regelsett som følges, men her er noen vanlige
            retningslinjer for et lovlig slag i 8-ball pool:
        </p>

        <ul class="list-disc pl-8 mb-4">
            <li>
                <strong>Køballkontakt:</strong> Køballen må treffe en av ballene
                i spillerens angitte gruppe (hele eller striper) først, og en ball
                fra den gruppen må lommes eller treffe en band etter kontakten.
            </li>
            <li>
                <strong>Objektballkontakt:</strong> Etter å ha truffet en ball fra
                spillerens angitte gruppe, kan køballen også treffe og lomme baller
                fra motstanderens gruppe (motstanderens baller), men det er ikke
                påkrevd.
            </li>
            <li>
                <strong>Lommet ball:</strong> Minst én ball må lommes lovlig på slaget.
                Hvis en spiller lommer en ball fra sin angitte gruppe, fortsetter
                de sin tur. Hvis en ball fra motstanderens gruppe blir lommet, blir
                det motstanderens tur.
            </li>
            <li>
                <strong>Ingen feil:</strong> Slaget må ikke resultere i en feil,
                noe som kan inkludere ting som å scratche (køballen lommes), ikke
                treffe noen ball, treffe køballen av bordet eller bryte andre spesifikke
                regler.
            </li>
        </ul>

        <p class="mb-4">
            Det er viktig å merke seg at forskjellige regelsett kan ha
            tilleggsvariasjoner og krav for et lovlig slag, som for eksempel om
            8-ball kan lommes tidlig eller kravet om å kalle slag. Det anbefales
            derfor alltid å avklare og enes om de spesifikke reglene før man
            spiller en runde 8-ball pool.
        </p>

        <hr class="mb-4" />

        <p class="mb-4">
            I spillet 8-ball pool, <strong class="eightball"
                >"ball in hand" refererer til en situasjon der en spiller har
                friheten til å plassere køballen hvor som helst på bordet før de
                tar skuddet sitt.
            </strong> Imidlertid kan de spesifikke reglene angående hvor spilleren
            kan plassere køballen etter en "ball in hand"-situasjon variere avhengig
            av regelsettet som følges.
        </p>

        <p class="mb-4">
            I mange regelsett, inkludert populære variasjoner av 8-ball pool som
            BCA (Billiard Congress of America) og WPA (World Pool-Billiard
            Association) sine regler, kan en spiller som har "ball in hand"
            plassere køballen hvor som helst på bordet, så lenge den ikke
            berører noen andre baller. Det er vanligvis ikke nødvendig at
            køballen spilles fra et spesifikt område som kjøkkenet (også kjent
            som "cue ball in the kitchen"-regelen).
        </p>

        <p class="mb-4">
            <strong>Kjøkkenet er en term</strong> som brukes i noen andre køsportspill,
            som snooker, for å referere til et spesifikt område på bordet der køballen
            må spilles fra etter visse regelbrudd eller andre spesifikke situasjoner.
            Imidlertid blir begrepet kjøkkenet ikke vanligvis brukt i sammenheng
            med 8-ball pool for å plassere køballen etter en "ball in hand"-situasjon.
        </p>

        <p>
            Det er alltid en god idé å avklare og bli enige om de spesifikke
            reglene som følges med motspilleren før man spiller en runde 8-ball
            pool for å sikre en rettferdig og hyggelig spillopplevelse.
        </p>
    </div>
{/if}
