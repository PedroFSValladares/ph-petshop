class Card extends HTMLElement {
    constructor() {
        super();

        const shadow = this.attachShadow({ mode: "open" });

        shadow.appendChild(this.build());
        shadow.appendChild(this.style());
    }

    build() {
        let componentRoot = document.createElement("div");

        let image = document.createElement("img");
        image.src = this.getAttribute("image") || "/images/default.jpg";

        let title = document.createElement("h3");
        title.textContent = this.getAttribute("title");

        componentRoot.appendChild(image);
        componentRoot.appendChild(title);

        return componentRoot;
    }

    style() {
        let style = document.createElement("style")

        style.textContent = `
            div{
                border-radius: .5rem;
                box-shadow: rgba(149, 157, 165, 0.2) 0px 8px 24px;
                cursor: pointer;
            }
            h3{
                font-size: 35px;
                color: purple;
                font-weight: normal;
                text-align: center;
                padding: 2rem;
                padding-bottom: 4.5rem;
            }
            img{
                border-top-left-radius: .5rem;
                border-top-right-radius: .5rem;
                width: 450px;
            }
        `;

        return style;
    }
}

customElements.define("card-anuncio", Card);